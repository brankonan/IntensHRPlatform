import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

function SearchPage() {
  const [name, setName] = useState("");
  const [allSkills, setAllSkills] = useState([]);
  const [selectedSkillIds, setSelectedSkillIds] = useState([]);
  const [results, setResults] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    api.get("/skills").then((res) => setAllSkills(res.data));
  }, []);

  const handleSkillToggle = (id) => {
    setSelectedSkillIds((prev) =>
      prev.includes(id) ? prev.filter((s) => s !== id) : [...prev, id],
    );
  };

  const handleSearch = async () => {
    const params = new URLSearchParams();
    if (name) params.append("name", name);
    selectedSkillIds.forEach((id) => params.append("skillIds", id));

    const res = await api.get(`/candidates/search?${params.toString()}`);
    setResults(res.data);
  };

  return (
    <div>
      <h1>Pretraga kandidata</h1>

      <div>
        <label>Ime: </label>
        <input value={name} onChange={(e) => setName(e.target.value)} />
      </div>

      <div>
        <label>Skillovi:</label>
        {allSkills.map((s) => (
          <label key={s.id}>
            <input
              type="checkbox"
              checked={selectedSkillIds.includes(s.id)}
              onChange={() => handleSkillToggle(s.id)}
            />
            {s.name}
          </label>
        ))}
      </div>

      <button onClick={handleSearch}>Pretrazi</button>
      <button onClick={() => navigate("/")}>Nazad</button>

      <h2>Rezultati</h2>
      <table border="1">
        <thead>
          <tr>
            <th>Ime</th>
            <th>Email</th>
            <th>Skillovi</th>
          </tr>
        </thead>
        <tbody>
          {results.map((c) => (
            <tr key={c.id}>
              <td>{c.fullName}</td>
              <td>{c.email}</td>
              <td>{c.skills.map((s) => s.name).join(", ")}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default SearchPage;
