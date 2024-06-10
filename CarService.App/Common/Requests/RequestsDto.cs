using CarService.App.Common.Users;
using CarService.Core.Requests;
using CarService.Core.Users;

namespace CarService.App.Common.Requests;

public class RequestsDto
{
	public RequestsDto(
		Guid id,
		Guid? clientId,
		UserInfo? client,
		string? carInfo,
		string? description,
		DateTime createTime,
		DateTime? visitTime,
		bool isTransferred,
		DateTime? completeTime,
		RequestPriority priority,
		RequestStatus status,
		List<WorkersDto> masters)
	{
		Id = id;
		ClientId = clientId;
		Client = client;
		CarInfo = carInfo;
		Description = description;
		CreateTime = createTime;
		VisitTime = visitTime;
		IsTransferred = isTransferred;
		CompleteTime = completeTime;
		Priority = priority;
		Status = status;
		Masters = masters;
	}

	public Guid Id { get; set; }

	public Guid? ClientId { get; set; }

	public UserInfo? Client { get; set; }

	public string? CarInfo { get; set; }

	public string? Description { get; set; }

	public DateTime CreateTime { get; set; }

	public DateTime? VisitTime { get; set; }

	public bool IsTransferred { get; set; } = false;

	public DateTime? CompleteTime { get; set; }

	public RequestPriority Priority { get; set; }

	public RequestStatus Status { get; set; }

	public List<WorkersDto> Masters { get; set; } = [];
}