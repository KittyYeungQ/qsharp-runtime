﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Simulation.QuantumProcessor
{

    public partial class QuantumProcessorDispatcher
    {
        public class QuantumProcessorDispatcherR1 : Quantum.Intrinsic.R1
        {
            private QuantumProcessorDispatcher Simulator { get; }

            public QuantumProcessorDispatcherR1(QuantumProcessorDispatcher m) : base(m)
            {
                this.Simulator = m;
            }

            public override Func<(double, Qubit), QVoid> Body => (_args) =>
            {
                var (angle, q1) = _args;
                Simulator.QuantumProcessor.R1(angle, q1);
                return QVoid.Instance;
            };

            public override Func<(double, Qubit), QVoid> AdjointBody => (_args) =>
            {
                var (angle, q1) = _args;
                return this.Body.Invoke((-angle, q1));
            };

            public override Func<(IQArray<Qubit>, ( double, Qubit)), QVoid> ControlledBody => (_args) =>
            {
                var (ctrls, (angle, q1)) = _args;
                Simulator.QuantumProcessor.ControlledR1(ctrls, angle, q1);
                return QVoid.Instance;
            };


            public override Func<(IQArray<Qubit>, (double, Qubit)), QVoid> ControlledAdjointBody => (_args) =>
            {
                var (ctrls, (angle, q1)) = _args;
                return this.ControlledBody.Invoke((ctrls, (-angle, q1)));
            };
        }
    }
}
