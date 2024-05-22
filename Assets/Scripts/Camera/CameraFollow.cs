using System.Threading.Tasks;
using UnityEngine;

namespace JokerGames.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; 
        public Vector3 offset; 
        public float smoothSpeed = 0.125f;
        public float rotationSmoothTime = 0.1f;
        
        private Vector3 velocity = Vector3.zero; 
        private Quaternion currentRotation;
        
        // ToDo update for performance drain
        public async Task SetTargetTransform(Transform transform)
        {
            target = transform;
          
            await Task.Delay(1000);
        }
        void LateUpdate()
        {
            if (target != null)
            {
                // Calculate Next position
                Vector3 desiredPosition = target.position + offset;
        
                // Smooth position
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        
                // update position
                transform.position = smoothedPosition;
        
                // Look at
                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
                currentRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime * Time.deltaTime);
                transform.rotation = currentRotation;
            }
        }
    }
}
