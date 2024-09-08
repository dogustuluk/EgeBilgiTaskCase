namespace EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter
{
    public class AddNewCharacterCommandResponse
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public List<int>? EpisodeIds { get; set; }
    }
}