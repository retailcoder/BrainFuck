using System;
using System.Text;

namespace BrainFuck
{
    public class ExecutionContext
    {
        public ExecutionContext(int memorySize = short.MaxValue, Func<int> onInput = null)
        {
            _onInput = onInput;
            _memory = new int[memorySize];
            _stdOutput = new StringBuilder();
        }

        private readonly int[] _memory;
        private readonly Func<int> _onInput;
        private readonly StringBuilder _stdOutput;

        private int _pointer;
        public int Pointer => _pointer;

        public int Value => _memory[_pointer];

        public string StdOut => _stdOutput.ToString();

        public bool IsTrue(int position = -1)
        {
            return (position == -1 ? _memory[_pointer] : _memory[position]) != 0;
        }

        public void MoveLeft()
        {
            if (_pointer == 0)
            {
                _pointer = _memory.Length;
            }
            else
            {
                _pointer--;
            }
        }

        public void MoveRight()
        {
            if (_pointer == _memory.Length)
            {
                _pointer = 0;
            }
            else
            {
                _pointer++;
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
            _stdOutput.Append((char)_memory[_pointer]);
        }

        public void Input()
        {
            _memory[_pointer] = _onInput?.Invoke() ?? Console.Read();
        }
    }
}