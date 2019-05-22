using Newtonsoft.Json.Linq;

namespace CRM.Apps.Forms {
    public interface IForm<TEntity> {
        // TDomainType Compose();
        // JObject Serialize();
        void Deserialize(JObject json);
        TEntity Compose();
    }
}

namespace CRM.Apps.Views {
    public interface IView<TEntity> {
        JObject Serialize();
        void Decompose(TEntity entity);
        // TODO: Flattening to Json is performed automatically by controller.
    }
}