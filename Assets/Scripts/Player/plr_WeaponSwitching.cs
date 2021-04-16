using UnityEngine;

namespace Player
{
    public class plr_WeaponSwitching : MonoBehaviour
    {
        private InputSystem controls;
        [Header("Weapons")]
        public GameObject glock19;
        public GameObject Axe;
        public GameObject Hands;

        void Awake()
        {
            controls = new InputSystem();
            controls.Enable();
            
            

            controls.Player.Weapon1.performed += e => Weapon1();
            controls.Player.Weapon2.performed += e => Weapon2();
            controls.Player.Weapon3.performed += e => Weapon3();
        }

        private void Weapon1()
        {
            Axe.SetActive(true);
            glock19.SetActive(false);
            Hands.SetActive(false);
        }

        private void Weapon2()
        {
            Axe.SetActive(false);
            glock19.SetActive(true);
            Hands.SetActive(false);
        }

        private void Weapon3()
        {
            Axe.SetActive(false);
            glock19.SetActive(false);
            Hands.SetActive(true);
        }
    }
}