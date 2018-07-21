﻿using Microsoft.AspNetCore.Blazor.Components;
using System;

namespace Blazor.Fluxor
{
	/// <summary>
	/// A class that is injected into Blazor components/pages that provides access
	/// to an <see cref="IFeature{TState}"/> state.
	/// </summary>
	/// <typeparam name="TState"></typeparam>
	public class State<TState> : IState<TState>
	{
		private readonly IFeature<TState> Feature;

		/// <summary>
		/// Creates an instance of the state holder
		/// </summary>
		/// <param name="feature">The feature that contains the state</param>
		public State(IFeature<TState> feature)
		{
			Feature = feature;
		}

		/// <see cref="IState{TState}.Value"/>
		public TState Value => Feature.State;

		/// <see cref="IState.Subscribe(BlazorComponent)"/>
		public void Subscribe(BlazorComponent subscriber) => Feature.Subscribe(subscriber);
	}
}
