using ESP.Types;

namespace ESP.Parser; 

public interface EventParser {
    public Task<Event> parse(object evt);
}