﻿using System;
using Avalonia.Data;

namespace Avalonia.PropertyStore
{
    /// <summary>
    /// Holds values in a <see cref="ValueStore"/> set by one of the SetValue or AddBinding
    /// overloads with non-LocalValue priority.
    /// </summary>
    internal class ImmediateValueFrame : ValueFrame
    {
        public ImmediateValueFrame(BindingPriority priority)
        {
            Priority = priority;
        }

        public TypedBindingEntry<T> AddBinding<T>(
            StyledPropertyBase<T> property,
            IObservable<BindingValue<T>> source)
        {
            var e = new TypedBindingEntry<T>(this, property, source);
            Add(e);
            return e;
        }

        public TypedBindingEntry<T> AddBinding<T>(
            StyledPropertyBase<T> property,
            IObservable<T> source)
        {
            var e = new TypedBindingEntry<T>(this, property, source);
            Add(e);
            return e;
        }

        public SourceUntypedBindingEntry<T> AddBinding<T>(
            StyledPropertyBase<T> property,
            IObservable<object?> source)
        {
            var e = new SourceUntypedBindingEntry<T>(this, property, source);
            Add(e);
            return e;
        }

        public ImmediateValueEntry<T> AddValue<T>(StyledPropertyBase<T> property, T value)
        {
            var e = new ImmediateValueEntry<T>(this, property, value);
            Add(e);
            return e;
        }

        public void OnEntryDisposed(IValueEntry value)
        {
            Remove(value.Property);
            Owner?.OnValueEntryRemoved(this, value.Property);
        }

        protected override bool GetIsActive(out bool hasChanged)
        {
            hasChanged = false;
            return true;
        }
    }
}
