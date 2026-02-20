import React, { useState } from "react";
import "./App.css";

function App() {
  const [formData, setFormData] = useState({
    monthlySalary: "",
    otherIncome: "",
    monthlyExpenses: "",
    existingDebtPayments: "",
    carPrice: "",
    deposit: "",
    interestRate: "",
    loanTermMonths: "",
  });

  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const analyzeFinance = async () => {
    setLoading(true);
    setError(null);
    setResult(null);

    try {
      const response = await fetch("http://localhost:5206/api/Finance/analyze", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        throw new Error(`Server error: ${response.status}`);
      }

      const data = await response.json();
      setResult(data);
    } catch (err) {
      console.error(err);
      setError("Failed to fetch data from backend. Check console for details.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="App">
      <h1>AutoFinance AI - Loan Readiness Analyzer</h1>

      <div className="form">
        {Object.keys(formData).map((key) => (
          <div key={key} style={{ marginBottom: "10px" }}>
            <label>{key}:</label>
            <input
              type="number"
              name={key}
              value={formData[key]}
              onChange={handleChange}
            />
          </div>
        ))}

        <button onClick={analyzeFinance} disabled={loading}>
          {loading ? "Analyzing..." : "Analyze"}
        </button>

        {error && <p style={{ color: "red" }}>{error}</p>}
      </div>

      {result && (
        <div className="results" style={{ marginTop: "20px" }}>
          <h2>Results:</h2>
          <p>Total Income: {result.totalIncome}</p>
          <p>Estimated Installment: {result.estimatedInstallmentFormatted || result.estimatedInstallment}</p>
          <p>Debt-to-Income Ratio: {result.debtToIncomeRatioFormatted || result.debtToIncomeRatio}</p>
          <p>Risk Level: {result.riskLevel}</p>
          <p>Approval Probability: {result.approvalProbability}%</p>
          <p>Suggested Car Price: {result.suggestedCarPrice}</p>
          <p>Advice: {result.advice}</p>
        </div>
      )}
    </div>
  );
}

export default App;