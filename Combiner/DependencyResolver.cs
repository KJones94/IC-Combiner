namespace Combiner
{
	using System;

	using Ninject;

	public class DependencyResolver
	{
		private static readonly Lazy<DependencyResolver> Lazy = new Lazy<DependencyResolver>(() => new DependencyResolver());

		private readonly StandardKernel m_Kernel;

		private DependencyResolver()
		{
			m_Kernel = new StandardKernel();
			m_Kernel.Bind<Database>().ToSelf().InSingletonScope();
			m_Kernel.Bind<CreatureDataVM>().ToSelf().InSingletonScope();
			m_Kernel.Bind<FiltersVM>().ToSelf().InSingletonScope();

			m_Kernel.Bind<ImportExportHandler>().ToSelf().InSingletonScope();
			m_Kernel.Bind<CreatureCsvWriter>().ToSelf().InSingletonScope();
		}

		public TOut Get<TOut>()
		{
			return this.m_Kernel.Get<TOut>();
		}

		public static DependencyResolver Instance => Lazy.Value;
	}
}
