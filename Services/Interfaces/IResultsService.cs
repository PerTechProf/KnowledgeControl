﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;

namespace KnowledgeControl.Services.Interfaces
{
    public interface IResultsService
    {
        CertificateModel GetCertificate(int testId);
    }
}