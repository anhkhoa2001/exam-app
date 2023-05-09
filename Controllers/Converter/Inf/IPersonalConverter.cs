using ExamApp.Controllers.DTO;
using ExamApp.Models;
using FirebaseAdmin.Auth;

namespace ExamApp.Controllers.Converter.Inf;

public interface IPersonalConverter : IBaseConverter<Personal, PersonalDTO>
{
    public Personal ConvertDTO2Model(UserRecord source);
}