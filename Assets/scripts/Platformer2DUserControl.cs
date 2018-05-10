using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
        public int playerNumber;
        private string jumpMap;
        private string moveMap;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if(playerNumber==2)
            {
                jumpMap = "Jump2";
            }
            else if(playerNumber == 1) {
                jumpMap = "Jump";
            }
            if(!jump)
            // Read the jump input in Update so button presses aren't missed.
            jump = CrossPlatformInputManager.GetButtonDown(jumpMap);
        }

        private void FixedUpdate()
        {
            if (playerNumber == 2)
            {
                moveMap = "Horizontal1";
            }
            else if (playerNumber == 1)
            {
                moveMap = "Horizontal";
            }
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis(moveMap);
            // Pass all parameters to the character control script.
            character.Move(h, crouch, jump);
            jump = false;
        }
    }
}