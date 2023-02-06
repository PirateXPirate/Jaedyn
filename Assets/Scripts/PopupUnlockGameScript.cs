using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUnlockGameScript : MonoBehaviour
{
    public Text txtHead;
    public Text txtDesc;
    public Text txtPig;
    public Text txtETC;
    public Text txtClose;

    private void OnEnable() {
        if (Utils.isUnlock) {
            txtHead.text = "ขอขอบพระคุณ";
            txtDesc.text = "คุณได้ปลดล็อคเกมแล้ว\r\nสามารถเล่นเกมได้ทุกแผนที่";
            txtPig.text = "เวอร์ชันเต็ม";
            txtClose.text = "ปิด";
        }
    }
}
