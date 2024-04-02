using System;
using SpaceTask.GameCore.MVP.Contracts;

namespace SpaceTask.GameCore.MVP.Realization.Projectile.Contracts
{
	public interface IProjectile : IObservable, INotificator, IDisposable
	{
		
	}
}
