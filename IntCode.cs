using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 
{
    class IntCode 
    {
        int[] program;
        
        public IntCode(string program) {
            this.program = program.Split(",").Select(i => int.Parse(i)).ToArray();
            Reset();
        }

        private IntCode() {
        }

        public IntCode Clone(){
            var ret = new IntCode() { program = program };
            ret.Reset();
            return ret;
        }

        int[] data;
        int ix;

        int op;
        Queue<int> modes;

        Queue<int> inputs = new Queue<int>();

        public void Reset() {
            this.data = (int[]) program.Clone();
            ix = 0;
        }

        public int[] Run(params int[] inputs) {
            this.inputs = new Queue<int>(inputs);
            Reset();
            List<int> outputs = new List<int>();
            try {
                while (true) {
                    outputs.Add(Run());
                }
            }
            catch (HaltException) {
                return outputs.ToArray();
            }
        }

        public void AddInput(int i) {
            inputs.Enqueue(i);
        }

        public int RunToOutput() {
            return Run();
        }

        int Run() {
            
            while (ix < data.Length) {
                var opcode = data[ix++];
                op = opcode % 100;
                modes = new Queue<int>(new[] {
                    (opcode / 100) % 10,
                    (opcode / 1000) % 10,
                    opcode / 10000});

                if (op == 99) {
                    throw new HaltException();
                }

                switch (op) {
                    case 1:
                        put( get() + get());
                        break;
                    case 2:
                        put(get() * get());
                        break;                        
                    case 3:
                        put(inputs.Dequeue());
                        break;
                    case 4:
                        return get();
                    case 5:
                        if (get() != 0) {
                            ix = get();
                        }
                        else {
                            ix++;
                        }
                        break;
                    case 6:
                        if (get() == 0) {
                            ix = get();
                        }
                        else {
                            ix++;
                        }
                        break;
                    case 7:
                        if (get() < get()) {
                            put(1);
                        } 
                        else {
                            put(0);
                        }
                        break;
                    case 8:
                        if (get() == get()) {
                            put(1);
                        } 
                        else {
                            put(0);
                        }
                        break;
                }
            }

            throw new InvalidOperationException();
        }

        private void put(int value) {
            data[data[ix++]] = value;
        }

        private int get() {
            if (modes.Dequeue() == 1) {
                return data[ix++];
            } 
            return data[data[ix++]];
        }

        public class HaltException : Exception {}

    }
}