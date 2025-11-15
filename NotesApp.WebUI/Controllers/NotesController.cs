using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Services;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Application.DTOs;

namespace NotesApp.WebUI.Controllers;

public class NotesController : Controller
{
    private readonly INoteService _notes;
    public NotesController(INoteService notes) => _notes = notes;

    public async Task<IActionResult> Index()
    {
        var notes = await _notes.GetAllAsync();
        return View(notes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var dto = await _notes.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(dto);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string Title, string Content, int Priority, string? Tags)
    {
        var tagNames = ParseTags(Tags);
        var id = await _notes.CreateAsync(Title, Content, (Priority)Priority, tagNames);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _notes.GetByIdAsync(id);
        if (dto == null) return NotFound();
        var vm = new EditNoteViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            Priority = dto.Priority,
            Tags = string.Join(", ", dto.Tags)
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditNoteViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var tags = ParseTags(vm.Tags);
        await _notes.UpdateAsync(vm.Id, vm.Title, vm.Content, (Priority)vm.Priority, tags);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var dto = await _notes.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return View(dto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _notes.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private static IEnumerable<string> ParseTags(string? tags)
        => string.IsNullOrWhiteSpace(tags)
            ? Array.Empty<string>()
            : tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                  .Where(s => !string.IsNullOrWhiteSpace(s))
                  .Select(s => s.ToLowerInvariant());
}
