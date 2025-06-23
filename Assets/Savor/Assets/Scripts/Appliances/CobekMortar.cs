using System.Collections.Generic;
using Undercooked.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace Undercooked.Appliances
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CobekMortar : Interactable, IPickable
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform sambal; // Objek visual sambal

        private const int MaxNumberIngredients = 3;
        private Material _sambalMaterial;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private readonly List<Ingredient> _ingredients = new List<Ingredient>(MaxNumberIngredients);

        public List<Ingredient> Ingredients => _ingredients;
        public bool IsEmpty() => _ingredients.Count == 0;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            #if UNITY_EDITOR
                Assert.IsNotNull(_rigidbody);
                Assert.IsNotNull(_collider);
                Assert.IsNotNull(meshRenderer);
                Assert.IsNotNull(sambal);
            #endif

            Setup();
        }

        private void Setup()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            _sambalMaterial = sambal.gameObject.GetComponent<MeshRenderer>()?.material;

            #if UNITY_EDITOR
                Assert.IsNotNull(_sambalMaterial);
            #endif

            DisableSambal();
        }

        public bool AddIngredients(List<Ingredient> ingredients)
        {
            if (!IsEmpty()) return false;
            if (ingredients.Count < 1 || ingredients.Count > MaxNumberIngredients) return false;
            _ingredients.AddRange(ingredients);

            foreach (var ingredient in _ingredients)
            {
                ingredient.transform.SetParent(Slot);
                ingredient.transform.SetPositionAndRotation(Slot.transform.position, Quaternion.identity);
            }

            if (CheckSambalIngredients(ingredients))
            {
                EnableSambal(ingredients[0]);
            }
            return true;
        }

        public void RemoveAllIngredients()
        {
            DisableSambal();
            _ingredients.Clear();
        }

        /// <summary>
        /// Cek bahan sambal: minimal 1, maksimal 3, dan harus sama jenisnya (misal semua cabai).
        /// </summary>
        public static bool CheckSambalIngredients(IReadOnlyList<Ingredient> ingredients)
        {
            if (ingredients == null || ingredients.Count < 1 || ingredients.Count > 3)
            {
                return false;
            }
            var type = ingredients[0].Type;
            for (int i = 1; i < ingredients.Count; i++)
            {
                if (ingredients[i].Type != type)
                    return false;
            }
            return true;
        }

        private void EnableSambal(in Ingredient ingredientSample)
        {
            sambal.gameObject.SetActive(true);
            _sambalMaterial.color = ingredientSample.BaseColor;
        }

        private void DisableSambal()
        {
            sambal.gameObject.SetActive(false);
        }

        public void Pick()
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }

        public void Drop()
        {
            gameObject.transform.SetParent(null);
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
        }

        public override bool TryToDropIntoSlot(IPickable pickableToDrop)
        {
            if (pickableToDrop == null) return false;
            switch (pickableToDrop)
            {
                case Ingredient ingredient:
                    if (this.IsEmpty() == false) return false;
                    this.AddIngredients(new List<Ingredient> { ingredient });
                    return true;
                case CobekMortar cobek:
                    if (this.IsEmpty() == false) return false;
                    this.AddIngredients(cobek.Ingredients);
                    cobek.RemoveAllIngredients();
                    return true;
                default:
                    Debug.LogWarning("[CobekMortar] Drop not recognized", this);
                    break;
            }
            return false;
        }

        public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
        {
            if (playerHoldPickable == null) return null;
            switch (playerHoldPickable)
            {
                case Ingredient ingredient:
                    // Bisa diambil jika sudah jadi sambal
                    break;
                case CobekMortar cobek:
                    if (cobek.IsEmpty())
                    {
                        if (this.IsEmpty()) return null;
                        cobek.AddIngredients(this._ingredients);
                    }
                    break;
            }
            return null;
        }
    }
}
