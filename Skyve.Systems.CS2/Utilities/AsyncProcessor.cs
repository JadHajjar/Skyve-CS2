namespace Skyve.Systems.CS2.Utilities;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class AsyncProcessor
{
	private readonly ConcurrentStack<TaskWrapperBase> _taskStack = new();
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private bool _isProcessing;

	public bool IsBusy => _isProcessing || _taskStack.Count > 0;

	public event Action? TasksCompleted;

	public async Task Queue(Func<Task> taskFunc)
	{
		var wrapper = new TaskWrapper(taskFunc);

		_taskStack.Push(wrapper);

		_ = ProcessTasks();

		await wrapper.TaskCompletionSource.Task;
	}

	public async Task<T> Queue<T>(Func<Task<T>> taskFunc)
	{
		var wrapper = new TaskWrapper<T>(taskFunc);

		_taskStack.Push(wrapper);

		_ = ProcessTasks();

		return await wrapper.TaskCompletionSource.Task;
	}

	private async Task ProcessTasks()
	{
		await _semaphore.WaitAsync();

		try
		{
			_isProcessing = true;

			while (_taskStack.TryPop(out var taskWrapper))
			{
				await taskWrapper.ExecuteAsync();
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

	private abstract class TaskWrapperBase
	{
		public abstract Task ExecuteAsync();
	}

	private class TaskWrapper(Func<Task> taskFunc) : TaskWrapperBase
	{
		public TaskCompletionSource<object?> TaskCompletionSource { get; } = new();

		public override async Task ExecuteAsync()
		{
			try
			{
				await taskFunc();

				TaskCompletionSource.SetResult(null);
			}
			catch (Exception ex)
			{
				TaskCompletionSource.SetException(ex);
			}
		}
	}

	private class TaskWrapper<T>(Func<Task<T>> taskFunc) : TaskWrapperBase
	{
		public TaskCompletionSource<T> TaskCompletionSource { get; } = new();

		public override async Task ExecuteAsync()
		{
			try
			{
				var result = await taskFunc();

				TaskCompletionSource.SetResult(result);
			}
			catch (Exception ex)
			{
				TaskCompletionSource.SetException(ex);
			}
		}
	}
}

