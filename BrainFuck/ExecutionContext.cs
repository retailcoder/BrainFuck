using System;

namespace BrainFuck
{
    public class ExecutionContext
    {
        public ExecutionContext(int memorySize = short.MaxValue)
        {
            _memory = new int[memorySize];
        }

        private readonly int[] _memory;

        private int _pointer;
        public int Pointer => _pointer;

        public bool IsTrue(int index)
        {
            return _memory[index] != 0;
        }

        public void MoveLeft()
        {
            _pointer--;
            if (_pointer < 0)
            {
                throw new InvalidOperationException();
            }
        }

        public void MoveRight()
        {
            _pointer++;
            if (_pointer > short.MaxValue)
            {
                throw new InvalidOperationException();
            }
        }

        public void Increment()
        {
            _memory[_pointer] += 1;
        }

        public void Decrement()
        {
            _memory[_pointer] -= 1;
        }

        public void Output()
        {
            Console.Write((char)_memory[_pointer]);
        }

        public void Input()
        {
            _memory[_pointer] = Console.Read();
        }
    }
}