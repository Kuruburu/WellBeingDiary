using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.T4Templating;
using System.Reflection;
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
                Title = string.IsNullOrWhiteSpace(diaryNoteDto.Title) ? $"Note from {DateTime.UtcNow.ToString("dd-MM-yyyy")}" : diaryNoteDto.Title,
                Text = diaryNoteDto.Text,
                IsPublic = diaryNoteDto.IsPublic,
                Rating = diaryNoteDto.Rating,
                CreationDate = DateTime.UtcNow
            };
        }
    }
}
