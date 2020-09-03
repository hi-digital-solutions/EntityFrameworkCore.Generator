﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EntityFrameworkCore.Generator
{
    public class UniqueNamer
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _names;

        public UniqueNamer()
        {
            _names = new ConcurrentDictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            Comparer = StringComparer.OrdinalIgnoreCase;

            // add existing
            UniqueContextName("ChangeTracker");
            UniqueContextName("Configuration");
            UniqueContextName("Database");
            UniqueContextName("InternalContext");
        }

        public IEqualityComparer<string> Comparer { get; set; }

        public string UniqueName(string bucketName, string name)
        {
            var hashSet = _names.GetOrAdd(bucketName, k => new HashSet<string>(Comparer));
            string result = MakeUnique(name, hashSet.Contains);
            hashSet.Add(result);

            return result;
        }

        public string UniqueClassName(string className)
        {
            return UniqueClassName("", className);
        }
        
        public string UniqueClassName(string @namespace, string className)
        {
            string globalClassName = "global::ClassName::" + @namespace;
            return UniqueName(globalClassName, className);
        }

        public string UniqueModelName(string @namespace, string className)
        {
            string globalClassName = "global::ModelClass::" + @namespace;
            return UniqueName(globalClassName, className);
        }

        public string UniqueContextName(string name)
        {
            return UniqueContextName("", name);
        }
        
        public string UniqueContextName(string @namespace, string name)
        {
            string globalContextname = "global::ContextName::" + @namespace;
            return UniqueName(globalContextname, name);
        }

        public string UniqueRelationshipName(string name)
        {
            const string globalContextname = "global::RelationshipName";
            return UniqueName(globalContextname, name);
        }

        public string MakeUnique(string name, Func<string, bool> exists)
        {
            string uniqueName = name;
            int count = 1;

            while (exists(uniqueName))
                uniqueName = string.Concat(name, count++);

            return uniqueName;
        }

    }
}
