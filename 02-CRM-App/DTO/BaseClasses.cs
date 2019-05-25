using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {
    public abstract class Form<TEntity> where TEntity : new() {

        // Deserialization
        public Form() {}
        public Form(JObject json) {}

        public virtual JObject Serialize() =>
            JObject.FromObject(this);

        public virtual TEntity Compose() =>
            new TEntity();
    }
}

namespace CRM.Apps.Views {
    public abstract class View<TEntity> {

        // Decomposition
        public View() {}
        public View(TEntity entity) {}

        public JObject Serialize() =>
            JObject.FromObject(this);
    }
}

// TODO: Utilizes automatic serialization from mvc controller?