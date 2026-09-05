import { useState } from 'react'
import { getPing } from './api/ping'
import type { PingResult } from './models/PingResult'
import { getCount } from './api/count'
import type { CountResult } from './models/CountResult'
import './App.css'

function App() {
  const [result, setResult] = useState<PingResult | null>(null)
  const [count, setCount] = useState<CountResult | null>(null)

  async function handlePing() {
    const pingResult = await getPing()
    setResult(pingResult)
    setCount(null)
  }

  async function handleCount() {
    const countResult = await getCount()
    setCount(countResult)
    setResult(null)
  }

  return (
    <main className="app">
      <aside className="api-menu" aria-label="API menu">
        <button
          className={`api-button${result ? ' is-selected' : ''}`}
          aria-pressed={Boolean(result)}
          onClick={handlePing}
        >
          Ping
        </button>

        <button
          className={`api-button${count ? ' is-selected' : ''}`}
          aria-pressed={Boolean(count)}
          onClick={handleCount}
        >
          Count
        </button>
      </aside>

      <section className="output" aria-label="API output" aria-live="polite">
        {result && (
          <div className="result-card">
            <p>
              <span className="result-label">Status:</span>
              <span className="result-value">{result.status}</span>
            </p>
            <p>
              <span className="result-label">Timestamp:</span>
              <span className="result-value">{result.timestamp}</span>
            </p>
          </div>
        )}
        {count && (
          <div className="result-card">
            <p>
              <span className="result-label">Number of earthquakes in the last 30 days:</span>
              <span className="result-value">{count.count}</span>
            </p>
          </div>
        )}
      </section>
    </main>
  )
}

export default App
