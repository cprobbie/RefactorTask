namespace RefactorThis.Core.DTOs.Requests;

public abstract record BaseOptionRequest(
    string Name, 
    string Description);