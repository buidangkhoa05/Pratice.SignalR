using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI
{
    public class DataRequest
    {
        public int CollectedDataTypeId { get; set; }

        public string DataValue { get; set; } = null!;

        public string DataUnit { get; set; } = null!;

        public string DeviceSerialNumber { get; set; } = null!;
    }
}
