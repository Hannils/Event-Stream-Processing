using ClassLibrary.Types;

namespace ClassLibrary.Parser; 

public interface EventParser {
    public Task<Event> parse(object evt);
}