import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

function AddSkillPage() {
  const navigate = useNavigate();
  const [name, setName] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("/skills", { name });
      navigate("/");
    } catch (err) {
      setError(err.response?.data?.message || "Greska");
    }
  };

  return (
    <div>
      <h1>Dodaj skill</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <label>Naziv skilla</label>
        <input
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <button type="submit">Sacuvaj</button>
        <button type="button" onClick={() => navigate("/")}>
          Nazad
        </button>
      </form>
    </div>
  );
}

export default AddSkillPage;
