import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

function AddCandidatePage() {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    fullName: "",
    email: "",
    contactNumber: "",
    dateOfBirth: "",
  });
  const [error, setError] = useState("");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post("/candidates", form);
      navigate("/");
    } catch (err) {
      setError(err.response?.data?.message || "Greska");
    }
  };

  return (
    <div>
      <h1>Dodaj kandidata</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Ime i prezime</label>
          <input name="fullName" onChange={handleChange} required />
        </div>
        <div>
          <label>Email</label>
          <input name="email" type="email" onChange={handleChange} required />
        </div>
        <div>
          <label>Kontakt</label>
          <input name="contactNumber" onChange={handleChange} required />
        </div>
        <div>
          <label>Datum rodjenja</label>
          <input
            name="dateOfBirth"
            type="date"
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit">Sacuvaj</button>
        <button type="button" onClick={() => navigate("/")}>
          Nazad
        </button>
      </form>
    </div>
  );
}

export default AddCandidatePage;
