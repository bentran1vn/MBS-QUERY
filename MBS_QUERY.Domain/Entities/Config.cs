﻿using System.ComponentModel.DataAnnotations;

namespace MBS_QUERY.Domain.Entities;

public class Config
{
    [Key]
    public string Key { get; set; }
    public int Value { get; set; }
}