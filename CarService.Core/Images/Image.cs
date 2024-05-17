using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Core.Images;

public class Image
{
    public Guid Id { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public byte[] Data { get; private set; } = Array.Empty<byte>();
}