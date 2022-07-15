using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dyelaga {
    public class DiceButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnDrop(PointerEventData eventData)
        {
            transform.position = eventData.position;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            
            var diceSlotIndex = results.FindIndex(x => x.gameObject.GetComponent("DiceSlot") != null);
            if (diceSlotIndex != -1) {
                var diceSlot = results[diceSlotIndex];
                var diceSlotObject = diceSlot.gameObject;
                //var script = diceSlot.GetComponent("DiceSlot") as DiceSlot;
                SendMessageUpwards("TaskDieDroppedOn", (diceSlotObject, gameObject));
                // Add to the item
            }
        }
    }
}