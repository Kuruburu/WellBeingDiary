using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.T4Templating;
using WellBeingDiary.Dtos.DiaryNote;
using WellBeingDiary.Entities;

namespace WellBeingDiary.Mappers
{
    public static class DiaryNoteMappers
    { 
        public static DiaryNoteDto ToDiaryNoteDto(this DiaryNote diaryNote)
        {
            return new DiaryNoteDto
            {
                Id = diaryNote.Id,
                Title = diaryNote.Title,
                Text = diaryNote.Text,
                IsPublic = diaryNote.IsPublic,
                Rating = diaryNote.Rating       
            };
        }

        public static DiaryNote ToDiaryNoteFromCreateDto(this CreateDiaryNoteRequestDto diaryNoteDto)
        {
            return new DiaryNote 
            { 
                Title = diaryNoteDto.Title,
                Text = diaryNoteDto.Text,
                IsPublic = diaryNoteDto.IsPublic,
                Rating = diaryNoteDto.Rating,
                CreationDate = DateTime.UtcNow
            };
        }
    }
}
