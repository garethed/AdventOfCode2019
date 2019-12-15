using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019 
{
    class IntCode 
    {
        long[] program;
        
        public IntCode(string program) {
            this.program = program.Split(",").Select(i => long.Parse(i)).ToArray();
            Reset();
        }

        private IntCode() {
        }

        public IntCode Clone(){
            var ret = new IntCode() { program = program };
            ret.Reset();
            return ret;
        }

        long[] data;
        long ix;
        long rb;

        long op;
        Queue<long> modes;

        Queue<long> inputs = new Queue<long>();

        public void Reset() {
            this.data = new long[8192];
            program.CopyTo(data, 0);
            
            ix = 0;
            rb = 0;
        }

        public long[] Run(params long[] inputs) {
            this.inputs = new Queue<long>(inputs);
            Reset();
            List<long> outputs = new List<long>();
            try {
                while (true) {
                    outputs.Add(Run());
                }
            }
            catch (HaltException) {
                return outputs.ToArray();
            }
        }

        public void AddInput(long i) {
            inputs.Enqueue(i);
        }

        public long RunToOutput() {
            return Run();
        }

        long Run() {
            
            while (ix < data.Length) {
                var opcode = data[ix++];
                op = opcode % 100;
                modes = new Queue<long>(new[] {
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
                    case 9:
                        rb += get();
                        break;

                }
            }

            throw new InvalidOperationException();
        }

        private void put(long value) {
            var offset = (modes.Dequeue() == 2 ? rb : 0);
            data[offset + data[ix++]] = value;
        }

        private long get() {
            long mode = modes.Dequeue();
            long value = data[ix++];
            if (mode == 2) {
                return data[rb + value];
            }
            else if (mode == 1) {
                return value;
            } 
            return data[value];
        }

        public class HaltException : Exception {}

    }
}