﻿using System.Threading.Tasks;

namespace Blazor.Fluxor
{
	/// <summary>
	/// Classes that implement this interface may be registered with the store. Whenever an action is dispatched
	/// the store will execute all effects registered as observers of type of action dispatched
	/// <seealso cref="IStore.AddEffect(IEffect)"/>
	/// </summary>
	public interface IEffect
	{
		/// <summary>
		/// This method is executed by the store immediately after an action is dispatched. You must first
		/// indicate which action type this effect is interested in by calling <see cref="IStore.AddEffect(IEffect)"/>
		/// </summary>
		/// <param name="action"></param>
		/// <returns>An array of actions that the effect wants the store to process. This can be null or empty.</returns>
		Task<IAction[]> HandleAsync(IAction action);
		/// <summary>
		/// Indicates whether or not the effect should react to a specific action dispatched through the store
		/// </summary>
		/// <param name="action">The action that is being dispatched through the store</param>
		/// <returns>True if the <see cref="HandleAsync(IAction)"/> method should be called</returns>
		bool ShouldReactToAction(IAction action);
	}

}
