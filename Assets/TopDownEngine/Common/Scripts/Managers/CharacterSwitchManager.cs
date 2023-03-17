using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.UI;
using Suriyun.MCS;
using System.Collections;
using DG.Tweening;


namespace MoreMountains.TopDownEngine
{
	/// <summary>
	/// Add this component to an empty object in your scene, and when you'll press the SwitchCharacter button (P by default, change that in Unity's InputManager settings), 
	/// your main character will be replaced by one of the prefabs in the list set on this component. You can decide the order (sequential or random), and have as many as you want.
	/// Note that this will change the whole prefab, not just the visuals. 
	/// If you're just after a visual change, look at the CharacterSwitchModel ability.
	/// If you want to swap characters between a bunch of characters within a scene, look at the CharacterSwap ability and CharacterSwapManager
	/// </summary>
	[AddComponentMenu("TopDown Engine/Managers/CharacterSwitchManager")]
	public class CharacterSwitchManager : TopDownMonoBehaviour
	{
		/// the possible orders the next character can be selected from
		public enum NextCharacterChoices { Sequential, Random , ChooseSpecificIndex}

		[Header("Character Switch")]
		[MMInformation("Add this component to an empty object in your scene, and when you'll press the SwitchCharacter button (P by default, change that in Unity's InputManager settings), your main character will be replaced by one of the prefabs in the list set on this component. You can decide the order (sequential or random), and have as many as you want.", MMInformationAttribute.InformationType.Info, false)]

		/// the list of possible characters prefabs to switch to
		[Tooltip("the list of possible characters prefabs to switch to")]
		public Character[] CharacterPrefabs;
		/// the order in which to pick the next character
		[Tooltip("the order in which to pick the next character")]
		public NextCharacterChoices NextCharacterChoice = NextCharacterChoices.Sequential;
		/// the initial (and at runtime, current) index of the character prefab
		[Tooltip("the initial (and at runtime, current) index of the character prefab")]
		public int CurrentIndex = 0;
		/// if this is true, current health value will be passed from character to character
		[Tooltip("if this is true, current health value will be passed from character to character")]
		public bool CommonHealth;

		[Header("Visual Effects")]
		/// a particle system to play when a character gets changed
		[Tooltip("a particle system to play when a character gets changed")]
		public ParticleSystem CharacterSwitchVFX;

		protected Character[] _instantiatedCharacters;
		protected ParticleSystem _instantiatedVFX;
		protected InputManager _inputManager;
		protected TopDownEngineEvent _switchEvent = new TopDownEngineEvent(TopDownEngineEventTypes.CharacterSwitch, null);

		[SerializeField] Image ActionButtonImage;
		[SerializeField] Image SkillButtonImage;
		[SerializeField] Image[] SwitchCharButtonImage;
		[SerializeField] Image[] SwitchCharOverlayImage;

		[SerializeField] Sprite[] ActionSpriteSet;
		[SerializeField] Sprite[] SkillSpriteSet;
		[SerializeField] Sprite[] CharSpriteSet;

		public float CoolDown;
		bool isCountdown = false;
		float countingNumber = 0;

        [SerializeField] private GameObject character0Button;
        [SerializeField] private GameObject character1Button;
        [SerializeField] private GameObject character2Button;
        [SerializeField] private Image character1OverlayImage;
        [SerializeField] private Image character2OverlayImage;

		private float blockerTime;
        private float spamBlockerDuration = 0.3f;
        private bool isSelecting;
		private Transform originalPosition;

		[SerializeField] private Transform button1TopPosition;
		[SerializeField] private Transform button2TopPosition;
        /// <summary>
        /// On Awake we grab our input manager and instantiate our characters and VFX
        /// </summary>
        protected virtual void Start()
		{
			_inputManager = FindObjectOfType(typeof(InputManager)) as InputManager;
			InstantiateCharacters();
			InstantiateVFX();
			SetOriginalPosition();
			SetUI(CurrentIndex);
			SetIndexListenner();
        }

		/// <summary>
		/// Instantiates and disables all characters in our list
		/// </summary>
		protected virtual void InstantiateCharacters()
		{
			_instantiatedCharacters = new Character[CharacterPrefabs.Length];

			for (int i = 0; i < CharacterPrefabs.Length; i++)
			{
				Character newCharacter = Instantiate(CharacterPrefabs[i]);
				newCharacter.name = "CharacterSwitch_" + i;
				newCharacter.gameObject.SetActive(false);
				_instantiatedCharacters[i] = newCharacter;
			}
		}

		/// <summary>
		/// Instantiates and disables the particle system if needed
		/// </summary>
		protected virtual void InstantiateVFX()
		{
			if (CharacterSwitchVFX != null)
			{
				_instantiatedVFX = Instantiate(CharacterSwitchVFX);
				_instantiatedVFX.Stop();
				_instantiatedVFX.gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// On Update we watch for our input
		/// </summary>
		protected virtual void Update()
		{
			if (_inputManager == null)
			{
				return;
			}

			if (_inputManager.SwitchCharacterButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				blockerTime = 0;
				SetCharacterSelectorPosition();
				SwitchCharacter();
			}

			if (isCountdown)
			{
                character0Button.SetActive(false);
                float tik =( 1 / CoolDown) * countingNumber;
				countingNumber += Time.deltaTime;

				for (int i = 0; i < SwitchCharOverlayImage.Length; i++)
				{
					SwitchCharOverlayImage[i].fillAmount = tik;
                }
				
				if (tik >= 1)
				{
					Stopcount();
				}
			}
			else
			{
                character0Button.SetActive(true);
            }
		}

        private void Stopcount()
        {
            for (int i = 0; i < SwitchCharOverlayImage.Length; i++)
            {
				SwitchCharButtonImage[i].gameObject.GetComponent<UniversalButton>().SetActiveState(true);
            }
            
			isCountdown = false;
			countingNumber = 0;
		}

        /// <summary>
        /// Switches to the next character in the list
        /// </summary>
        protected virtual void SwitchCharacter()
		{
			if (_instantiatedCharacters.Length <= 1)
			{
				return;
			}

			// we determine the next index
            if (NextCharacterChoice == NextCharacterChoices.Random)
			{
				CurrentIndex = UnityEngine.Random.Range(0, _instantiatedCharacters.Length);
			}
				
			if (NextCharacterChoice == NextCharacterChoices.Sequential)
			{
				CurrentIndex = CurrentIndex + 1;
				if (CurrentIndex >= _instantiatedCharacters.Length)
				{
					CurrentIndex = 0;
				}
				SetUI(CurrentIndex);
			}

			if (NextCharacterChoice == NextCharacterChoices.ChooseSpecificIndex)
			{
				SetIndexListenner();
                if (CurrentIndex >= _instantiatedCharacters.Length)
                {
                    CurrentIndex = 0;
                }
                SetUI(CurrentIndex);
            }

			
		
			var oldAngle = LevelManager.Instance.Players[0].transform.localEulerAngles;

			// we disable the old main character, and enable the new one
			LevelManager.Instance.Players[0].gameObject.SetActive(false);
			_instantiatedCharacters[CurrentIndex].gameObject.SetActive(true);

			// we move the new one at the old one's position
			_instantiatedCharacters[CurrentIndex].transform.position = LevelManager.Instance.Players[0].transform.position;
			_instantiatedCharacters[CurrentIndex].transform.rotation = LevelManager.Instance.Players[0].transform.rotation;

			// we keep the health if needed
			if (CommonHealth)
			{
				_instantiatedCharacters[CurrentIndex].gameObject.MMGetComponentNoAlloc<Health>().SetHealth(LevelManager.Instance.Players[0].gameObject.MMGetComponentNoAlloc<Health>().CurrentHealth);
			}

			// we put it in the same state the old one was in
			_instantiatedCharacters[CurrentIndex].MovementState.ChangeState(LevelManager.Instance.Players[0].MovementState.CurrentState);
			_instantiatedCharacters[CurrentIndex].ConditionState.ChangeState(LevelManager.Instance.Players[0].ConditionState.CurrentState);

			// we make it the current character
			LevelManager.Instance.Players[0] = _instantiatedCharacters[CurrentIndex];

			// we play our vfx
			if (_instantiatedVFX != null)
			{
				_instantiatedVFX.gameObject.SetActive(true);
				_instantiatedVFX.transform.position = _instantiatedCharacters[CurrentIndex].transform.position;
				_instantiatedVFX.Play();
			}
			LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().Reset();
			var orientation = LevelManager.Instance.Players[0].GetComponent<CharacterOrientation3D>();
			//LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().Reset();
				orientation.Face(oldAngle);

			// we trigger a switch event (for the camera to know, mostly)
			MMEventManager.TriggerEvent(_switchEvent);
			MMCameraEvent.Trigger(MMCameraEventTypes.RefreshAutoFocus, LevelManager.Instance.Players[0], null);
		}

        private void SetUI(int currentIndex)
        {
			ActionButtonImage.sprite = ActionSpriteSet[currentIndex];
			SkillButtonImage.sprite = SkillSpriteSet[currentIndex];

            for (int i = 0; i < CharSpriteSet.Length; i++)
            {
				var current = currentIndex + i;
				if (current >= CharSpriteSet.Length)
				{
                    current -= CharSpriteSet.Length;
                }

				SwitchCharButtonImage[i].sprite = CharSpriteSet[current];
				SwitchCharOverlayImage[i].sprite = CharSpriteSet[current];
				SwitchCharOverlayImage[i].fillAmount = 0;
				SwitchCharOverlayImage[i].GetComponentInParent<UniversalButton>().SetActiveState(false);
            }
			character0Button.GetComponent<Image>().sprite = CharSpriteSet[currentIndex];
            isCountdown = true;
		}

        public void SetCharacterSelectorPosition()
        {
			if (blockerTime + spamBlockerDuration > Time.unscaledTime) { return; }
			blockerTime = Time.unscaledTime;

            character1Button.gameObject.SetActive(true);
            character2Button.gameObject.SetActive(true);

            if (isSelecting)
            {
                isSelecting = false;
                character1Button.transform.DOMove(originalPosition.transform.position, 0.3f, false).OnComplete(SetInActive);
                character2Button.transform.DOMove(originalPosition.transform.position, 0.15f, false).OnComplete(SetInActive);
            }

            else
            {
                isSelecting = true;
				character1Button.transform.DOMove(button1TopPosition.position, 0.3f, false);
				character2Button.transform.DOMove(button2TopPosition.position, 0.3f, false);
            }

    

            void SetInActive()
            {
                character1Button.gameObject.SetActive(false);
                character2Button.gameObject.SetActive(false);
            }
        }

        private void SetOriginalPosition()
        {

			originalPosition = character0Button.transform;
           // button1TopPosition = new Vector3(button1OriginalPosition.x, button1OriginalPosition.y + offset1, button1OriginalPosition.z);

          //  button2TopPosition = new Vector3(button2OriginalPosition.x, button2OriginalPosition.y + offset2, button2OriginalPosition.z);
        }
        private void SetIndexListenner()
        {
            Button buttonIndex0 = character0Button.GetComponent<Button>();

			buttonIndex0.onClick.AddListener(SetCharacterSelectorPosition);
            character1Button.GetComponent<UniversalButton>().onPointerDown.AddListener(delegate { SetCurrentIndex(1); });
            character2Button.GetComponent<UniversalButton>().onPointerDown.AddListener(delegate { SetCurrentIndex(2); });
        }
        private void SetCurrentIndex(int buttonIndex)
        {
            CurrentIndex = CurrentIndex + buttonIndex;
            if (CurrentIndex >= _instantiatedCharacters.Length)
            {
                CurrentIndex = CurrentIndex - _instantiatedCharacters.Length;
            }

            character1Button.GetComponent<UniversalButton>().onPointerDown.RemoveAllListeners();
            character2Button.GetComponent<UniversalButton>().onPointerDown.RemoveAllListeners();
        }

     

    }
}