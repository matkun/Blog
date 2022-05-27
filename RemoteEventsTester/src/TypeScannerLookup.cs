using EPiServer.Framework.TypeScanner;
using System;
using System.Collections.Generic;

namespace RemoteEventsTester
{
    internal class TypeScannerLookup : ITypeScannerLookup
    {
        private readonly HashSet<Type> _types = new HashSet<Type>();

        private static readonly object _lock = new object();

        public IEnumerable<Type> AllTypes => _types;

        public TypeScannerLookup() { }

        public TypeScannerLookup(IEnumerable<Type> types)
        {
            _types = new HashSet<Type>(types);
        }

        public void Add(Type t)
        {
            lock (_lock)
            {
                _ = _types.Add(t);
            }
        }

        public void Delete(Type t) => _ = _types.Remove(t);
    }
}
