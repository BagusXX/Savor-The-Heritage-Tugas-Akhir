using System.Collections;
using Undercooked.Model;
using Undercooked.Player;
using UnityEngine;
using UnityEngine.Assertions;
using Slider = UnityEngine.UI.Slider;

namespace Undercooked.Appliances
{
    public class Grilling : Interactable
    {
        [SerializeField] private Transform knife;
        [SerializeField] private Slider slider;
        
        private float _finalProcessTime;
        private float _currentProcessTime;
        private Coroutine _grillCoroutine;
        private Ingredient _ingredient;
        private bool _isGrilling;

        public delegate void GrillingStatus(PlayerController playerController);
        public static event GrillingStatus OnGrillingStart;
        public static event GrillingStatus OnGrillingStop;

        protected override void Awake()
        {
            #if UNITY_EDITOR
                Assert.IsNotNull(slider);
                Assert.IsNotNull(slider);
            #endif
            
            base.Awake();
            slider.gameObject.SetActive(false);
        }

        public override void Interact(PlayerController playerController)
        {
            LastPlayerControllerInteracting = playerController; 
            base.Interact(playerController);
            if (CurrentPickable == null ||
                _ingredient == null ||
                _ingredient.Status != IngredientStatus.Processed) return;
            
            if (_grillCoroutine == null)
            {
                _finalProcessTime = _ingredient.ProcessTime;
                _currentProcessTime = 0f;
                slider.value = 0f;
                slider.gameObject.SetActive(true);
                StartGrillCoroutine();
                return;
            }

            if (_isGrilling == false)
            {
                StartGrillCoroutine();
            }
        }

        private void StartGrillCoroutine()
        {
            OnGrillingStart?.Invoke(LastPlayerControllerInteracting);
            _grillCoroutine = StartCoroutine(Grill());
        }

        private void StopGrillCoroutine()
        {
            OnGrillingStop?.Invoke(LastPlayerControllerInteracting);
            _isGrilling = false;
            if (_grillCoroutine != null) StopCoroutine(_grillCoroutine);
        }

        public override void ToggleHighlightOff()
        {
            base.ToggleHighlightOff();
            StopGrillCoroutine();
        }
        
        private IEnumerator Grill()
        {
            _isGrilling = true;
            while (_currentProcessTime < _finalProcessTime)
            {
                slider.value = _currentProcessTime / _finalProcessTime;
                _currentProcessTime += Time.deltaTime;
                yield return null;
            }

            // finished
            _ingredient.ChangeToProcessed();
            slider.gameObject.SetActive(false);
            _isGrilling = false;
            _grillCoroutine = null;
            OnGrillingStop?.Invoke(LastPlayerControllerInteracting);
        }
        
        public override bool TryToDropIntoSlot(IPickable pickableToDrop)
        {
            if (pickableToDrop is Ingredient)
            {
                return TryDropIfNotOccupied(pickableToDrop);
            }
            return false;
        }

        public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
        {
            // only allow Pickup after we finish grilling the ingredient. Essentially locking it in place.
            if (CurrentPickable == null) return null;
            if (_grillCoroutine != null) return null;
            
            var output = CurrentPickable;
            _ingredient = null;
            var interactable = CurrentPickable as Interactable;
            interactable?.ToggleHighlightOff();
            CurrentPickable = null;
            knife.gameObject.SetActive(true);
            return output;
        }
        
        private bool TryDropIfNotOccupied(IPickable pickable)
        {
            if (CurrentPickable != null) return false;
            CurrentPickable = pickable;
            _ingredient = pickable as Ingredient;
            if (_ingredient == null) return false;

            _finalProcessTime = _ingredient.ProcessTime;
            
            CurrentPickable.gameObject.transform.SetParent(Slot);
            CurrentPickable.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
            knife.gameObject.SetActive(false);
            return true;
        }
    }
}