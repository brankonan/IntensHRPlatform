import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import api from "../api/axios";

function CandidatesDetailsPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [candidate, setCandidate] = useState(null);
  const [allSkills, setAllSkills] = useState([]);
  const [selectedSkillId, setSelectedSkillId] = useState("");

  const fetchCandidate = () => {
    api.get(`/candidates/${id}`).then((res) => setCandidate(res.data));
  };

  useEffect(() => {
    fetchCandidate();
    api.get("/skills").then((res) => setAllSkills(res.data));
  }, []);

  const handleAddSkill = async () => {
    if (!selectedSkillId) return;
    try {
      await api.put(`/candidates/${id}/skills/${selectedSkillId}`);
      fetchCandidate();
    } catch (err) {
      alert(err.response?.data?.message || "Greska");
    }
  };

  const handleRemoveSkill = async (skillId) => {
    try {
      await api.delete(`/candidates/${id}/skills/${skillId}`);
      fetchCandidate();
    } catch (err) {
      alert(err.response?.data?.message || "Greska");
    }
  };

  if (!candidate) return <p>Ucitavanje...</p>;

  return (
    <div>
      <h1>{candidate.fullName}</h1>
      <p>Email: {candidate.email}</p>
      <p>Kontakt: {candidate.contactNumber}</p>
      <p>
        Datum rodjenja: {new Date(candidate.dateOfBirth).toLocaleDateString()}
      </p>

      <h2>Skillovi</h2>
      <ul>
        {candidate.skills.map((s) => (
          <li key={s.id}>
            {s.name}
            <button onClick={() => handleRemoveSkill(s.id)}>Ukloni</button>
          </li>
        ))}
      </ul>

      <h3>Dodaj skill</h3>
      <select
        onChange={(e) => setSelectedSkillId(e.target.value)}
        value={selectedSkillId}
      >
        <option value=""> Izaberi skill </option>
        {allSkills.map((s) => (
          <option key={s.id} value={s.id}>
            {s.name}
          </option>
        ))}
      </select>
      <button onClick={handleAddSkill}>Dodaj</button>

      <br />
      <br />
      <button onClick={() => navigate("/")}>Nazad</button>
    </div>
  );
}

export default CandidatesDetailsPage;
