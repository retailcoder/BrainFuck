using System;
using System.Text;

namespace BrainFuck
{
    public class ExecutionContext
    {
        public ExecutionContext(int memorySize = short.MaxValue, Action<char> onOutput = null, Func<int> onInput = null)
        {
            _onOutput = onOutput;
            _onInput = onInput;
            _memory = new int[memorySize];
            _stdOutput = new StringBuilder();
        }

        private readonly int[] _memory;
        private readonly StringBuilder _stdOutput;

        private readonly Action<char> _onOutput;
        private readonly Func<int> _onInput;

        public int Size => _memory.Length;
        public int Pointer { get; private set; }
        public int Value => _memory[Pointer];
        public bool IsTrue(int? position = null) => _memory[position ?? Pointer] != 0;

        public string StdOut => _stdOutput.ToString();

        public void MoveLeft()
        {
            if (Pointer == 0)
            {
                Pointer = _memory.Length;
            }
            else
            {
                Pointer--;
            }
        }

        public void MoveRight()
        {
            if (Pointer == _memory.Length)
            {
                Pointer = 0;
            }
            else
            {
                Pointer++;
            }
        }

        public void Increment() => _memory[Pointer]++;
        public void Decrement() => _memory[Pointer]--;

        public void Output()
        {
            var output = (char) _memory[Pointer];
            _onOutput?.Invoke(output);
            _stdOutput.Append(output);
        }

        public void Input()
        {
            _memory[Pointer] = _onInput?.Invoke() ?? Console.Read();
        }
    }
}