using UnityEngine;

namespace Game.Bomb
{
    public class BombThrower : MonoBehaviour
    {
        [SerializeField] private BombsHolder _bombsHolder;
        [SerializeField] private Transform _instantiatePoint;
        [SerializeField] private float _apogeeAdditionalHeight;

        public bool TryThrow(Vector3 target)
        {
            if (_bombsHolder.TryGetBombPrefab(out Bomb prefab) == false)
                return false;

            Vector3 velocity = CalculateVelocity(_instantiatePoint.position, target);
            Bomb bomb = Instantiate(prefab, _instantiatePoint.position, Quaternion.identity);
            bomb.SetVelocity(velocity);

            return true;
        }

        private Vector3 CalculateVelocity(Vector3 start, Vector3 end)
        {
            float apogee = CalculateApogee(start, end);
            float risingTime = CalculateTime(apogee - start.y);
            float fallingTime = CalculateTime(apogee - end.y);

            Vector3 horizontalVelocity = CalculateHorizontalVelocity(start, end, risingTime + fallingTime);
            Vector3 verticalVelocity = CalculateVerticalVelocity(risingTime);

            return horizontalVelocity + verticalVelocity;
        }

        private Vector3 CalculateHorizontalVelocity(Vector3 start, Vector3 end, float time)
        {
            Vector3 horizontalVelocity = end - start;
            horizontalVelocity.y = 0;
            horizontalVelocity /= time;

            return horizontalVelocity;
        }

        private Vector3 CalculateVerticalVelocity(float risingTime)
        {
            return -Physics.gravity * risingTime;
        }

        private float CalculateApogee(Vector3 startPoint, Vector3 endPoint)
        {
            return Mathf.Max(startPoint.y, endPoint.y) + _apogeeAdditionalHeight;
        }

        private float CalculateTime(float height)
        {
            float g = Mathf.Abs(Physics.gravity.y);
            float time = Mathf.Sqrt((2 * height) / g);

            return time;
        }
    }
}