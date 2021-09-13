using Interfaces;
using UnityEngine;

namespace ReverseTime.Commands
{
    public class MoveCommand : ICommand
    {
        private Transform _objectPosition;
        private Vector3 _position;
        private Vector3 _previousPosition;
    
        public MoveCommand(Transform objectPosition, Vector3 position)
        {
            _objectPosition = objectPosition;
            _position = position;
        }

        public void Execute()
        {
            _previousPosition = _objectPosition.transform.position;
            _objectPosition.transform.position = _position;
            /*Debug.Log("Previous poz: " + _previousPosition);
            Debug.Log("Object Poz: " + _objectPosition.transform.position);
            Debug.Log("Current Poz: " + _position);*/
        }

        public void Undo()
        {
            _objectPosition.transform.position = _previousPosition;
           // Debug.Log("Object Poz: " + _objectPosition.transform.position);
        }
    }
}