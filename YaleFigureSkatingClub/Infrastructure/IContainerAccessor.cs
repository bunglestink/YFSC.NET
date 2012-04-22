using System;
using Ninject;

namespace YaleFigureSkatingClub
{
	public interface IContainerAccessor
	{
		IKernel NinjectKernel { get; }
	}
}

