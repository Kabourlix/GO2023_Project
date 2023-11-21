// Copyrighted by team Rézoskour
// Created by alexandre buzon on 17

using UnityEngine;

namespace Rezoskour.Content
{
    public class DashUI : MonoBehaviour
    {
        [SerializeField] private GameObject dashUIPrefab;
        [SerializeField] private DashSystem dashSystem;
        [SerializeField] private GameObject dashList1;
        [SerializeField] private GameObject dashList2;
        private int nbDash;
        private int dashCount;

        // Start is called before the first frame update
        private void Start()
        {
            dashCount = 0;
            dashSystem.OnDashEvent += Move;
            //clear dash list
            foreach (Transform child in dashList1.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in dashList2.transform)
            {
                Destroy(child.gameObject);
            }

            nbDash = dashSystem.DashList.Count;
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nbDash);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            //change width of dashlist
            RectTransform rectTransform1 = dashList1.GetComponent<RectTransform>();
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nbDash);
            rectTransform1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);


            RectTransform rectTransform2 = dashList2.GetComponent<RectTransform>();
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nbDash);
            rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            for (int i = 0; i < nbDash; i++)
            {
                //create dash UI
                GameObject dashUIList1 = Instantiate(dashUIPrefab, dashList1.transform);
                GameObject dashUIList2 = Instantiate(dashUIPrefab, dashList2.transform);
            }
        }

        public void OnDestroy()
        {
            dashSystem.OnDashEvent -= MoveToIndex;
        }

        private void AddDash()
        {
            nbDash++;
            GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1f);
            //change width of dashlist
            dashList1.GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1f);

            dashList2.GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1f);

            //create dash UI
            GameObject dashUIList1 = Instantiate(dashUIPrefab, dashList1.transform);
            GameObject dashUIList2 = Instantiate(dashUIPrefab, dashList2.transform);

            MoveToIndex();
        }

        private void RemoveDash(int _index)
        {
            nbDash--;
            GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1);
            //change width of dashlist
            dashList1.GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1);

            dashList2.GetComponent<RectTransform>().sizeDelta = new Vector2(nbDash, 1);

            //destroy dash UI
            Destroy(dashList1.transform.GetChild(_index).gameObject);
            Destroy(dashList2.transform.GetChild(_index).gameObject);
            MoveToIndex();
        }

        private void Move()
        {
            //int currentDashIndex = dashSystem.CurrentDashIndex;

            LeanTween.moveLocalX(dashList1, dashList1.transform.localPosition.x - 1, 0.2f).setOnComplete(() =>
            {
                if ((int)dashList1.transform.localPosition.x == -nbDash)
                {
                    dashList1.transform.localPosition = new Vector3(nbDash, 0f, 0f);
                }
            });
            LeanTween.moveLocalX(dashList2, dashList2.transform.localPosition.x - 1, 0.2f).setOnComplete(() =>
            {
                if ((int)dashList2.transform.localPosition.x == -nbDash)
                {
                    dashList2.transform.localPosition = new Vector3(nbDash, 0f, 0f);
                }
            });
        }

        private void MoveToIndex()
        {
            int currentDashIndex = dashSystem.CurrentDashIndex;
            while ((int)dashList1.transform.localPosition.x != -currentDashIndex ||
                   (int)dashList2.transform.localPosition.x != -currentDashIndex)
            {
                Move();
            }
        }
    }
}