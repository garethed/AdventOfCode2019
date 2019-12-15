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
        }

        int[] data;
        Queue<int> inputs;
        int ix;
        List<int> outputs = new List<int>();

        int op;
        Queue<int> modes;

        public int[] Run(params int[] inputs) {
            this.inputs = new Queue<int>(inputs);
            this.data = (int[]) program.Clone();
            outputs.Clear();
            return Run();
        }

        int[] Run() {
            
            ix = 0;
            while (ix < data.Length) {
                var opcode = data[ix++];
                op = opcode % 100;
                modes = new Queue<int>(new[] {
                    (opcode / 100) % 10,
                    (opcode / 1000) % 10,
                    opcode / 10000});

                if (op == 99) {
                    return outputs.ToArray();
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
                        outputs.Add(get());
                        break;
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

    }
}