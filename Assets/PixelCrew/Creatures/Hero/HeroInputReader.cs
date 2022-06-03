using PixelCrew.Creatures;
using PixelCrew.Creatures.Hero;
using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrew.UI.Widgets.StopMenu;

namespace PixelCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _hero.StartingThrow();
            }

            if (context.canceled)
            {
                _hero.CancledThrow();
            }
        }
    

        public void OnGameMenu(InputAction.CallbackContext context)
        {
            var gameMenu = FindObjectOfType<GameMenuWindow>();
            if (gameMenu != null) return;
            if (context.performed)
            {
                var window = Resources.Load<GameObject>("UI/GameMenuWindom");
                var canvas = GameObject.FindGameObjectWithTag("GameMenuCanvas");
                Instantiate(window, canvas.transform);
            }
        }

        public void OnHeal(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePotion();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }

        public void OnUsePerk(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UseActivePerk();
            }
        }

        public void UseCandle(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UseCandle();
            }
        }
    }
}