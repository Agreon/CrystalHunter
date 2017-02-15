using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AI.FiniteStateMachine
{
	public abstract class FSMState<T> {
		protected FSM<T> _machine;
		protected T _context;

		public FSMState() {}

		internal void setMachineAndContext( FSM<T> machine, T context ) {
			_machine = machine;
			_context = context;
			onInitialized();
		}
			
		/// <summary>
		/// called directly after the machine and context are set allowing the state to do any required setup
		/// </summary>
		public virtual void onInitialized() {}

		public virtual void begin() {}

		public virtual void reason() {}

		public abstract void update( float deltaTime );

		public virtual void end() {}
	
	}
}