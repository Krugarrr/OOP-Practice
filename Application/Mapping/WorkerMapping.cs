﻿using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class WorkerMapping
{
    public static WorkerDto AsDto(this Worker worker)
        => new WorkerDto(worker.Name, worker.Login, worker.Password);
}