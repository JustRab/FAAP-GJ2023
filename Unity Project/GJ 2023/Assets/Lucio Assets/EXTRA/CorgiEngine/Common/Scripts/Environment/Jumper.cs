using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Add this class to a platform to make it a jumping platform, a trampoline or whatever.
	/// It will automatically push any character that touches it up in the air.
	/// </summary>
	[AddComponentMenu("Corgi Engine/Environment/Jumper")]
	public class Jumper : MonoBehaviour 
	{
		/// the force of the jump induced by the platform
		public float JumpPlatformBoost = 40;

        protected CorgiController _controller;

		/// <summary>
		/// Triggered when a CorgiController touches the platform, applys a vertical force to it, propulsing it in the air.
		/// </summary>
		/// <param name="controller">The corgi controller that collides with the platform.</param>			
		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
            _controller = collider.GetComponent<CorgiController>();
			if (_controller == null)
            {
                return;
            }				
		}

        /// <summary>
        /// On exit we get rid of our controller
        /// </summary>
        /// <param name="collider"></param>
        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            if (_controller != null)
            {
                if (collider.gameObject == _controller.gameObject)
                {
                    _controller = null;
                }
            }            
        }

        /// <summary>
        /// On late update we set a force to our collider's controller if we have one
        /// </summary>
        protected virtual void LateUpdate()
        {
            if (_controller != null)
            {
                _controller.SetVerticalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -_controller.Parameters.Gravity));
            }            
        }
	}
}