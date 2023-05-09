namespace ExamApp.Controllers.Converter.Inf;

public interface IBaseConverter<MODEL, DTO>
{
    public MODEL ConvertDTO2Model(DTO dto);
    public DTO ConvertModel2DTO(MODEL model);
    public string validate(DTO dto);
}