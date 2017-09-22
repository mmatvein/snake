namespace ECS
{
    using IdType = System.Int32;

    public class Entity
    {
        public readonly IdType id;
        public readonly string humanReadableName;

        public Entity(IdType id, string humanReadableName)
        {
            this.id = id;
            this.humanReadableName = humanReadableName;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;
            return entity != null &&
                   id == entity.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}