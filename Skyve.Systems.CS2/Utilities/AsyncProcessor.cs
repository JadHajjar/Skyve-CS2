namespace Skyve.Systems.CS2.Utilities;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class AsyncProcessor
{
	private readonly ConcurrentStack<Func<Task>> _taskStack = new();
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private bool _isProcessing;

	public bool IsBusy => _isProcessing || _taskStack.Count > 0;

	public event Action? TasksCompleted;

	public async Task Queue(Func<Task> taskFunc)
	{
		var tcs = new TaskCompletionSource<object?>();

		_taskStack.Push(async () =>
		{
			try
			{
				await taskFunc();

				tcs.SetResult(null);
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}
		});

		_ = ProcessTasks();

		await tcs.Task;
	}

	public async Task<T> Queue<T>(Func<Task<T>> taskFunc)
	{
		var tcs = new TaskCompletionSource<T>();

		_taskStack.Push(async () =>
		{
			try
			{
				tcs.SetResult(await taskFunc());
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}
		});

		_ = ProcessTasks();

		return await tcs.Task;
	}

	private async Task ProcessTasks()
	{
		await _semaphore.WaitAsync();

		try
		{
			_isProcessing = true;

			while (_taskStack.TryPop(out var taskFunc))
			{
				await taskFunc();
			}
		}
		finally
		{
			_isProcessing = false;
			_semaphore.Release();

			if (!IsBusy)
			{
				TasksCompleted?.Invoke();
			}
		}
	}
}
