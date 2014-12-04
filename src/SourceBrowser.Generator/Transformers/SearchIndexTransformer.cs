﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceBrowser.Generator.Model;
using System.IO;
using SourceBrowser.Search;

namespace SourceBrowser.Generator.Transformers
{
    public class SearchIndexTransformer : AbstractWorkspaceVisitor
    {
        private string _username;
        private string _repository;

        public SearchIndexTransformer(string username, string repository)
        {
            _username = username;
            _repository = repository;
        }
        protected override void VisitDocument(DocumentModel documentModel)
        {
            var documentId = Path.Combine(_username, _repository, documentModel.RelativePath);
            var declarations = documentModel.Tokens.Where(n => n.IsDeclaration);

            foreach(var declaration in declarations)
            {
                //Add to index
                SearchIndex.AddDeclarationToIndex(documentId, declaration.FullName, declaration.LineNumber);
            }

            base.VisitDocument(documentModel);
        }
    }
}
