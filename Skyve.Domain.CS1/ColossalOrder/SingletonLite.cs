using Skyve.Domain.Systems;

using System;

namespace Skyve.Domain.CS1.ColossalOrder;
public abstract class SingletonLite<T>
		where T : SingletonLite<T>, new()
{
	protected static T? sInstance;
	private static readonly object lockObject = new(); // work around in case constructor was used by mistake.
	public static T instance
	{
		get
		{
			try
			{
				if (sInstance == null)
				{
					lock (lockObject)
					{
						sInstance = new T();
#if DEBUG
						ServiceCenter.Get<ILogger>().Debug("Created singleton of type " + typeof(T).Name + ". calling Awake() ...");
						sInstance.Awake();
						ServiceCenter.Get<ILogger>().Debug("Awake() finished for " + typeof(T).Name);
#else
						sInstance.Awake();
#endif
					}
				}

				return sInstance;
			}
			catch (Exception ex)
			{
				ServiceCenter.Get<ILogger>().Exception(ex, "");
				throw;
			}
		}
	}

	public static bool exists => sInstance != null;

	public static void Ensure()
	{
		_ = instance;
	}

	public virtual void Awake() { }
}

