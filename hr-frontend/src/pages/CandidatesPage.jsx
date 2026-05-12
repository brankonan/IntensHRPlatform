import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

function CandidatesPage() {
  const [candidates, setCandidates] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    api.get("/candidates").then((res) => setCandidates(res.data));
  }, []);

  const handleDelete = async (id) => {
    await api.delete(`/candidates/${id}`);
    setCandidates(candidates.filter((c) => c.id !== id));
  };

  return (
    <div>
      <h1>Kandidati</h1>
      <button onClick={() => navigate("/add")}>Dodaj Kandidata</button>
      <button onClick={() => navigate("/add-skill")}>Dodaj skill</button>
      <button onClick={() => navigate("/search")}>Pretraga</button>
      <table border="1">
        <thead>
          <tr>
            <th>Ime</th>
            <th>Email</th>
            <th>Kontakt</th>
            <th>Skillovi</th>
            <th>Akcije</th>
          </tr>
        </thead>
        <tbody>
          {candidates.map((c) => (
            <tr key={c.id}>
              <td>{c.fullName}</td>
              <td>{c.email}</td>
              <td>{c.contactNumber}</td>
              <td>{c.skills.map((s) => s.name).join(", ")}</td>
              <td>
                <button onClick={() => navigate(`/candidates/${c.id}`)}>
                  Detalji
                </button>
                <button onClick={() => handleDelete(c.id)}>Obrisi</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CandidatesPage;
