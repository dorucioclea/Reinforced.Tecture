﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Reinforced.Tecture.Testing.Data.SyntaxGeneration.Collection.Strategies
{
    class ObjectInitializerStrategy : ICollectionCreationStrategy
    {
        private readonly Type _collectionType;

        public ObjectInitializerStrategy(Type collectionType)
        {
            _collectionType = collectionType;
        }

        private IEnumerable<SyntaxNodeOrToken> WithComas(IEnumerable<ExpressionSyntax> items)
        {
            bool first = true;
            foreach (var expressionSyntax in items)
            {
                if (!first) yield return Token(SyntaxKind.CommaToken);
                else first = false;
                yield return expressionSyntax;
            }
        }

        public ExpressionSyntax Generate(IEnumerable<ExpressionSyntax> members, HashSet<string> usings)
        {
            var ct = _collectionType;

            if (_collectionType.IsAbstract || _collectionType.IsInterface)
            {
                ct = typeof(List<object>);
            }
            if (_collectionType.ElementType().IsAnonymousType())
            {
                var genCollection = _collectionType.GetGenericTypeDefinition();
                ct = genCollection.MakeGenericType(typeof(Dictionary<string,object>));
            }


            return ObjectCreationExpression(ct.TypeName(usings))
                .WithInitializer(InitializerExpression(SyntaxKind.CollectionInitializerExpression,
                    SeparatedList<ExpressionSyntax>(WithComas(members))));
        }
    }
}